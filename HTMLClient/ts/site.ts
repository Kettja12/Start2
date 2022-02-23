async function fetchHtmlAsText(url) {
    return await (await fetch(url)).text();
}

function isAdmin(claims: Array<claimType>): boolean {
    for (let i = 0; i < claims.length; i++) {
        if (claims[i].claimType === "UserGroups"
            && claims[i].claimValue.includes('Admin')) {
            return true;
        }
        return false;
    }
}

function showError(e, message) {
    e.innerHTML = "<p>" + message + "</p>";
    e.style.display = 'block';
    setTimeout(function () {
        e.style.display = 'none';
    }, 5000)
}

function translate(d, s) {
    let r = d[s];
    if (r === undefined) return s;
    return r;
}

async function loadControl(controlname:string,path:string) {
    let content = document.getElementById('div'+controlname);
    if (content === null) {
        content = document.createElement('div');
        content.innerHTML = await fetchHtmlAsText(path + controlname + ".html?" + Math.random());
        document.body.append(content.firstElementChild);
        content = document.getElementById("div" + controlname);
    }
    return content;

}

function loadScript(src, callback,path) {

    var script = <HTMLScriptElement>document.createElement('script');
    script.type = 'text/javascript';
    script.onload = () => {
        if (callback) callback(path);
    };
    script.src = src;
    document.head.appendChild(script);
}
