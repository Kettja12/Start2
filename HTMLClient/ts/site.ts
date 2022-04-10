async function fetchHtmlAsText(url) {
    return await (await fetch(url)).text();
}

function isAdmin(claims: Array<claimType>): boolean {
    for (let i = 0; i < claims.length; i++) {
        if (claims[i].claimType === "UserGroups"
            && claims[i].claimValue.includes('Admin')) {
            return true;
        }
    }
    return false;
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

async function loadControl(controlname: string) {
    let content = document.createElement('div');
    content.innerHTML = await fetchHtmlAsText(controlname + ".html?" + Math.random());
    content = <HTMLDivElement>content.firstElementChild;
    document.body.append(content);
    return content;
}

function loadModule(src, fname,e) {
    var script = <HTMLScriptElement>document.createElement('script');
    script.type = 'module';
    //script.type = 'text/javascript';
    script.onload = () => {
        //fname();
        if (fname !== undefined) {
            runByName(fname, e)
        }
    };
    script.src = src + '?x=' + Math.random();
    document.head.appendChild(script);
}
function loadScript(src, fname, e) {
    var script = <HTMLScriptElement>document.createElement('script');
    script.type = 'text/javascript';
    script.onload = () => {
        if (fname !== undefined) {
            runByName(fname, e)
        }
    };
    script.src = src + '?x=' + Math.random();
    document.head.appendChild(script);
}

function runByName(fname: string, e) {
    window[fname](e);
}

function toggleClassOn(control: string, cssClass: string) {
    let content = <HTMLElement> document.getElementById(control);
    if (content.classList.contains(cssClass) === false) {
        content.classList.toggle(cssClass);
    }
}

function toggleClassOff(control: string, cssClass: string) {
    let content = <HTMLElement>document.getElementById(control);
    if (content.classList.contains(cssClass)) {
        content.classList.toggle(cssClass);
    }
}

function capitalizeFirstLetter(string) {
    return string.charAt(0).toUpperCase() + string.slice(1);
}