class MenuItem {
    title = "";
    indent = 0;

    /**
     * @type {MenuItem[]}
     */
    parent = null;

    /**
     * @type {MenuItem[]}
     */
    children = [];

    hasChildren() {
        return this.children.length > 0;
    }
}

class Parser {
    /**
     * @param {string} text 
     */
    process(text) {
        const lines = text.split("\n");

        let items = [];

        lines.forEach(element => {
            let item = this.processLine(element);
            if (item) {
                items.push(item);
            }
        });

        return items;
    }

    /**
     * @param {string} line
     * @returns {MenuItem?}
     */
    processLine(line) {
        line = line.trimEnd();

        if (line.trim().length === 0) {
            return null;
        }

        const regex = /^(\s*)(.*)\s*$/;
        const match = regex.exec(line);

        if (match === null) {
            throw new Error("Invalid line: " + line);
        }

        const indent = match[1];
        const title = match[2];

        // count tabs and spaces in indent
        const tabs = (indent.match(/\t/g) || []).length;
        const spaces = (indent.match(/ /g) || []).length;

        const menuItem = new MenuItem();
        menuItem.title = title;
        menuItem.indent = tabs + spaces / 4;

        return menuItem;
    }
}

class TreeBuilder {
    root = null;

    /**
     * @type {MenuItem[]}
     */
    last = null;

    /**
     * @type {MenuItem[]}
     */
    stack = [];

    /**
     * @type {MenuItem[]}
     */
    items = [];

    /**
     * @param {MenuItem[]} items 
     */
    build(items) {
        // clone items
        this.items = items.slice();

        // expect root item to be at index 0
        const item = this.items.shift();

        if (item.indent !== 0) {
            console.log(item);
            throw new Error("First item must have indent of 0.");
        }

        this.root = item;
        this.last = item;

        while (this.items.length > 0) {
            this.#processNext(item);
        }
    }

    #processNext() {
        if (this.items.length === 0) {
            return;
        }

        const item = this.items.shift();
        let parent = this.peakStack();

        if (item.indent === 0) {
            throw new Error("Multiple root items found. See console for details.");
        } else {
            const indentDiff = item.indent - this.last.indent;

            if (indentDiff === 0) {
                // parent is already setup
            } else if (indentDiff === 1) {
                // last item is parent of current item
                parent = this.last;
                this.stack.push(parent);
            } else if (indentDiff > 1) {
                throw new Error("Invalid indent. See console for details.");
            } else {
                // negative indentDiff is the number of levels to go up
                const levels = -1 * indentDiff;
                for (let i = 0; i < levels; i++) {
                    this.stack.pop();
                    parent = this.peakStack();
                }
            }

            parent.children.push(item);
            item.parent = parent;
        }

        this.last = item;
    }

    peakStack() {
        return this.stack[this.stack.length - 1];
    }
}

class PlantUmlGen {
    statesText = "";
    transitionsText = "";

    /**
     * @param {MenuItem} item 
     */
    #makeOptionName(item) {
        if (item.hasChildren())
            return `${item.parent.title}__${item.title}`;
        return `${item.title}`;
    }

    /**
     * @param {MenuItem} state
     */
    genState(state, indent = "") {
        let str = `${indent}state ${state.title}`;

        if (state.hasChildren() == false) {
            str += "\n";
        } else {
            str += " {\n";
            const children = state.children;

            const innerIndent = indent + "    ";

            // need to output initial transition like `[*] -> State1`
            const initialChild = children[0];
            str += `${innerIndent}[*] -> ${this.#makeOptionName(initialChild)}\n`;

            children.forEach((child, index) => {
                str += `${innerIndent}state ${this.#makeOptionName(child)}\n`;
            });

            let needsSpacer = true;

            children.forEach((child, index) => {
                if (child.children.length > 0) {
                    if (needsSpacer) {
                        str += "\n";
                        needsSpacer = false;
                    }

                    str += this.genState(child, innerIndent);
                }
            });
            str += indent + "}\n";
        }

        return str;
    }

    /**
     * Recursive function.
     * @param {MenuItem} state
     */
    genStateTransitions(state) {
        let str = ``;

        const children = state.children;

        if (children.length == 0)
            return str;

        str += "\n";
        str += `' ${state.title}\n`;

        const parent = state.parent;

        // need left/right transitions
        if (parent !== null) {
            str += `${this.#makeOptionName(state)} -> ${state.title} : RIGHT\n`;
            str += `${state.title} -> ${this.#makeOptionName(state)} : LEFT\n`;
        }

        // down transitions
        for (let i = 1; i < children.length; i++) {
            const a = children[i - 1];
            const b = children[i];
            str += `${this.#makeOptionName(a)} --> ${this.#makeOptionName(b)} : DOWN\n`;
        }

        // up transitions
        for (let i = children.length - 1; i >= 1; i--) {
            const a = children[i];
            const b = children[i - 1];
            str += `${this.#makeOptionName(a)} --> ${this.#makeOptionName(b)} : UP\n`;
        }

        // now descend into children 
        for (let i = 0; i < children.length; i++) {
            const c = children[i];
            str += this.genStateTransitions(c);
        }

        return str;
    }

    /**
     * Recursive function.
     * @param {MenuItem} state
     */
    genMenuPosition(state) {
        let str = ``;

        for (let i = 0; i < state.children.length; i++) {
            const c = state.children[i];

            if (i == 0) {
                str += `${this.#makeOptionName(c)}: enter / menu_at_top();\n`;
            } else if (i == state.children.length - 1) {
                str += `${this.#makeOptionName(c)}: enter / menu_at_bottom();\n`;
            } else {
                str += `${this.#makeOptionName(c)}: enter / menu_in_middle();\n`;
            }
        }

        for (let i = 0; i < state.children.length; i++) {
            const c = state.children[i];
            str += this.genMenuPosition(c);
        }

        return str;
    }

    /**
     * Recursive function.
     * @param {MenuItem} state
     */
    genHandlers(state) {
        let str = ``;

        if (state.parent !== null) {
            if (state.hasChildren()) {
                str += `${this.#makeOptionName(state)}: enter / show_${this.#makeOptionName(state)}();\n`;
            } else {
                str += `${state.title}: enter / show_${state.title}();\n`;
            }
        }

        // now descend into children 
        for (let i = 0; i < state.children.length; i++) {
            const c = state.children[i];
            str += this.genHandlers(c);
        }

        return str;
    }
}

function process(text) {
    const parser = new Parser();
    const items = parser.process(text);

    const builder = new TreeBuilder();
    builder.build(items);

    console.log(builder.root);

    const gen = new PlantUmlGen();
    let output = gen.genState(builder.root);
    output += gen.genStateTransitions(builder.root);

    output += "\n\n'EVENT HANDLERS\n";
    output += gen.genHandlers(builder.root);

    output += "\n\n'MENU POSITION HANDLERS\n";
    output += gen.genMenuPosition(builder.root);

    return output;
}