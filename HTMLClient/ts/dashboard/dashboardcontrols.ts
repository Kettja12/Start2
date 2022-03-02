import { getDashboardItems } from "./dashboardapi.js"
export { }
document.addEventListener('DOMContentLoaded', async () => {
    if (stateservice == undefined) {
        window.location.href = '/';
    }
    document.getElementById("savecontrol").
        addEventListener("click", () => savecontrol(), false);
    await loadDasboardControls();
});
async function loadDasboardControls() {
    await search();
};

let controllist: dashboardItemType[];
let currentcontrol = -1;

async function search() {
    var ul = document.getElementById('dashboarditems');
    ul.innerHTML = "";
    resetcontrol();
    controllist = await  getDashboardItems();
    let response = await apiGet("Dashboard/GetDashboardItems");
    if (response != null) {
        await setControls();
    }
}

async function  setControls() {
        var ul = document.getElementById('dashboarditems');
        controllist.forEach(function (control, index) {
            var li = document.createElement('li');
            var span = document.createElement('span');
            span.style.cssText = "display: inline-block;width:50px"
            span.appendChild(document.createTextNode(control.control));
            li.appendChild(span);
            li.setAttribute("index", index.toString());
            li.addEventListener("click",(e)=>select(e), false);
            ul.appendChild(li);
        });

}
function select(e) {
        resetcontrol();
        var i = e.currentTarget.getAttribute("index");
        currentcontrol = i;
        console.log(currentcontrol);
        //(document.getElementById('title') as HTMLInputElement).value = controllist[i].title;
        (document.getElementById('control') as HTMLInputElement).value = controllist[i].control;
        (document.getElementById('userGroups') as HTMLInputElement).value = controllist[i].userGroups;
        (document.getElementById('inuse') as HTMLInputElement).checked = controllist[i].inUse;

    }
function  resetcontrol() {
        (document.getElementById('title') as HTMLInputElement).value = "";
        (document.getElementById('control') as HTMLInputElement).value = "";
        (document.getElementById('userGroups') as HTMLInputElement).value = "";
        (document.getElementById('inuse') as HTMLInputElement).checked = false;
    }


async function savecontrol() {
    var control = controllist[currentcontrol]
    control.inUse = (document.getElementById('inuse') as HTMLInputElement).checked;
    control.userGroups = (document.getElementById('userGroups') as HTMLInputElement).value;
    var response = await apiPost("/Dashboard/SaveDashboardItem", control);
    if (response.status === "OK") {
        //showInfoMessage(response.message, "w3-green");
        return;
    }
    //showInfoMessage(response.message, "w3-yellow");
};



