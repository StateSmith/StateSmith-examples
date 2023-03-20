class smHighlighter
{
    /**
     * @param {string} string
     */
    #escapeRegExp(string) {
        return string.replace(/[.*+?^${}()|[\]\\]/g, "\\$&"); // $& means the whole matched string
    }      

    /**
     * @param {string} line
     */
    highlight(line)
    {
        //Enter OnOffSm.
        let match = line.match(/^Enter ([^.]+)[.]/d);

        if (match)
        {
            let [start, end] = match.indices[1];
            let result = "";
            result += line.substring(0, start);
            result += "<span class='stateName'>";
            result += match[1];
            result += "</span>";
            result += line.substring(end);
            return result;
        }

        return line;
    }
}
