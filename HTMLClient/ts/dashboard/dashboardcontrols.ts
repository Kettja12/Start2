import { getDashboardItems, saveDashboardItem, getMessage } from "./dashboardapi.js"
let translations = {
    'DashboardItem save failed.': 'DashboardItem save failed.',
    'DashboardItem save success.': 'DashboardItem save success.'
}


document.addEventListener('DOMContentLoaded', async () => {
    let c = <HTMLElement> document.getElementById("translations");
    translations = JSON.parse(c.innerHTML);

    if (stateservice == undefined) {
        window.location.href = '/';
    }
    (<HTMLElement>document.getElementById("savecontrol")).
        addEventListener("click", () => savecontrol(), false);
    await loadDasboardControls();
});
async function loadDasboardControls() {
    await search();
};

let controllist: dashboardItemType[] | null;
let currentcontrol = -1;

async function search() {
    var ul = <HTMLElement>document.getElementById('dashboarditems');
    ul.innerHTML = "";
    resetcontrol();
    controllist = await getDashboardItems();
    if (controllist != null) {
        await setControls();
    }
}

async function setControls() {
    var ul = <HTMLElement>document.getElementById('dashboarditems');
    if (controllist != null) {
        controllist.forEach(function (control, index) {
            var li = document.createElement('li');
            var span = document.createElement('span');
            span.style.cssText = "display: inline-block;width:50px"
            span.appendChild(document.createTextNode(control.control));
            li.appendChild(span);
            li.setAttribute("index", index.toString());
            li.addEventListener("click", (e) => select(e), false);
            ul.appendChild(li);
        });
    }

}
function select(e) {
    if (controllist == null) return;
    resetcontrol();
    var i = e.currentTarget.getAttribute("index");
    currentcontrol = i;
    (document.getElementById('control') as HTMLInputElement).value = controllist[i].control;
    (document.getElementById('userGroups') as HTMLInputElement).value = controllist[i].userGroups;
    (document.getElementById('inuse') as HTMLInputElement).checked = controllist[i].inUse;

}
function resetcontrol() {
    (document.getElementById('control') as HTMLInputElement).value = "";
    (document.getElementById('userGroups') as HTMLInputElement).value = "";
    (document.getElementById('inuse') as HTMLInputElement).checked = false;
}


async function savecontrol() {
    if (controllist == null) return;
    let control: dashboardItemType = <dashboardItemType>controllist[currentcontrol]
    control.inUse = (document.getElementById('inuse') as HTMLInputElement).checked;
    control.userGroups = (document.getElementById('userGroups') as HTMLInputElement).value;
    let e = document.getElementById('infomessage');
    let response = await saveDashboardItem(control);
    if (response != null) {
        showError(e, translate(translations, 'DashboardItem save success.'));
        return;
    }
    let message = getMessage();
    if (message !== "")
        showError(e, translate(translations, message))
    else
        showError(e, translate(translations, "DashboardItem save failed."))

};



