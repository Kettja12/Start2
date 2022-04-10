export { initDashboard, removeItem }
window.removeItem = removeItem;
import { getDashboardItems } from "./dashboardapi.js"
//import { loadItem1 } from "./item1.js"
import { loadItem2 } from "./item2.js"
import { loadItem3 } from "./item3.js"
let controlTitles = {
    'Item1': 'Item 1',
    'Item2': 'Item 2',
    'Item3': 'Item 3'
}

let hiddenControlslist: dashboardItemType[];
let controlslist: dashboardItemType[] | null;
let sortlist: number[];
async function initDashboard() {
    let c = <HTMLElement>document.getElementById("controlTitles");
    controlTitles = JSON.parse(c.innerHTML);
    document.addEventListener("dragstart", function (e: DragEvent) {
        let source = <DataTransfer>e.dataTransfer;
        source.setData("Text", (e.target as HTMLDivElement).id);
        (e.target as HTMLDivElement).style.opacity = "0.4";
    })

    document.addEventListener("dragenter", function (e: DragEvent) {
        if ((e.target as HTMLDivElement).classList.contains("droptarget")) {
            (e.target as HTMLDivElement).style.opacity = "0.4";
        }
    });

    document.addEventListener("dragend", function (e: DragEvent) {
        (e.target as HTMLDivElement).style.opacity = "1";
    });
    document.addEventListener("dragover", function (e: DragEvent) {
        e.preventDefault();
    });

    document.addEventListener("dragleave", function (e: DragEvent) {
        if ((e.target as HTMLDivElement).classList.contains("droptarget")) {
            (e.target as HTMLDivElement).style.opacity = "1";
        }
    });

    document.addEventListener("drop", async (e: DragEvent) => {
        e.preventDefault();
        if ((e.target as HTMLDivElement).classList.contains("droptarget")) {
            (e.target as HTMLDivElement).style.opacity = "1";
            let dropTarget = <HTMLElement>document.getElementById((e.target as HTMLDivElement).title);
            let source = <DataTransfer>e.dataTransfer;
            let data = source.getData("Text");
            let dropControl = <HTMLElement>document.getElementById(data);
            dropControl.style.opacity = "1";
            let divDashboardItems = <HTMLElement>document.getElementById("divDashboardItems");
            if (await sortNodes(data, (e.target as HTMLDivElement).title))
                divDashboardItems.insertBefore(dropControl, dropTarget);
            else
                divDashboardItems.insertBefore(dropControl, dropTarget.nextSibling);
            saveClaim(sortlist.toString())

        }
    });
    await loadDashboard();
}

async function loadDashboard() {
    controlslist = await getDashboardItems();
    if (controlslist != null)
        await setControls();
}

async function setControls() {
    let divDashboardItems = <HTMLElement>document.getElementById("divDashboardItems");
    divDashboardItems.innerHTML = '';
    if (controlslist == null) return;
    sortlist = getSortlist();
    let itemlist = controlslist.filter((item) => {
        return item.inUse === true;
    });
    hiddenControlslist = itemlist.filter((item) => {
        return sortlist.includes(item.id) === false;
    });
    let divdashboard = <HTMLElement>document.getElementById("divDashboard");
    if (hiddenControlslist.length > 0) {

        let c = <HTMLElement>document.getElementById("hideHiddenControllist");
        c.addEventListener("click", () => {
            let element = <HTMLElement>document.getElementById("hiddenDasboardItems");
            element.classList.add("w3-hide");
        });

        divdashboard.addEventListener("contextmenu", (e) => showHiddenControlList(e));
    }
    else {
        divdashboard.removeEventListener("contextmenu", (e) => showHiddenControlList(e));
    }
    for (let j = 0; j < sortlist.length; j++) {
        for (let i = 0; i < itemlist.length; i++) {
            if (itemlist[i].id === sortlist[j]) {
                let control = itemlist[i];
                let content = await loadControl("Dashboard/" + control.control)
                divDashboardItems.appendChild(content);
                if (control.control === 'Item1') {
                    //loadItem1();
                    loadScript('../scripts/dashboard/' + control.control + '.js',
                        'load' + control.control, "loadItem1");
                }
                if (control.control === 'Item2') {
                    loadItem2();
                }
                if (control.control === 'Item3') {
                    loadItem3();
                }
            }
        }
    }
}

function getSortlist() {
    let sortlist = [];
    stateservice.user.claims.forEach(function (claim) {
        if (claim.claimType === "DashboardItems") {
            sortlist = parseIntList(claim.claimValue);
            return;
        }
    });
    return sortlist;
}
function parseIntList(s) {
    var result = s.split(',').map(function (item) {
        return parseInt(item, 10);
    });
    return result;
}
async function showHiddenControlList(e: MouseEvent) {
    let element = <HTMLElement>document.getElementById("hiddenControllist");
    element.innerHTML = "";
    for (var i = 0; i < hiddenControlslist.length; i++) {
        let li = document.createElement('li');
        let span = document.createElement('span');
        let control = hiddenControlslist[i].control;
        span.appendChild(document.createTextNode(controlTitles[control]));
        li.appendChild(span);
        li.setAttribute("index", i.toString());
        li.addEventListener("click", (e) => selectHiddenControl(e), false)
        element.appendChild(li);
    }
    var xpos = e.pageX - 300;
    var ypos = e.pageY;
    element = <HTMLElement>document.getElementById("hiddenDasboardItems");
    if (element != null) {
        element.style.position = 'absolute';
        element.style.left = xpos + "px";
        element.style.top = ypos + "px";
        element.classList.remove("w3-hide");
    }
    e.preventDefault();
}

async function selectHiddenControl(e: MouseEvent) {
    let element = <HTMLElement>document.getElementById("hiddenDasboardItems");
    element.classList.add("w3-hide");
    let target = <HTMLElement>e.currentTarget;
    var i = <string>target.getAttribute("index");
    sortlist[sortlist.length] = hiddenControlslist[i].id;
    let s = sortlist.join();
    await saveClaim(s);
}
async function saveClaim(s) {
    let isset = false;
    let claim: claimType = null as any;
    stateservice.user.claims.forEach(function (oldClaim) {
        if (oldClaim.claimType === "DashboardItems") {
            oldClaim.claimValue = s;
            isset = true;
            claim = oldClaim;
        }
    });
    if (isset === false) {
        claim = {
            id: -1,
            claimType: "DashboardItems",
            claimValue: s,
            userId: stateservice.user.id
        }
        stateservice.user.claims.push(claim)
    }

    let response = await apiPost("account/saveclaim", claim);
    if (response.status === "OK") {
        //showInfoMessage(response.message, "w3-green");
    }
    else {
        //showInfoMessage(response.message, "w3-yellow");
    }
    localStorage.setItem('stateservice', JSON.stringify(stateservice));

}
async function removeItem(item) {

    if (controlslist == null) return;
    let removeitem = <dashboardItemType>controlslist.find((e) => { return e.control === item })
    let sl = sortlist.filter((item) => { return item != removeitem.id })
    let s = sl.join();
    await saveClaim(s);
    await setControls();
}

async function sortNodes(fromNode, toNode) {
    if (controlslist == null) return;
    let dragFromId = -1
    let dragToId = -1
    for (let j = 0; j < controlslist.length; j++) {
        if (controlslist[j].control == fromNode) {
            dragFromId = controlslist[j].id;
        }
        if (controlslist[j].control == toNode) {
            dragToId = controlslist[j].id;
        }

    }
    let dragFromPosition = -1
    let dragToPosition = -1
    for (let j = 0; j < sortlist.length; j++) {
        if (sortlist[j] == dragFromId) {
            dragFromPosition = j;
        }
        if (sortlist[j] == dragToId) {
            dragToPosition = j;
        }

    }
    let newSortlist: number[] = [];
    let index = 0;
    if (dragFromPosition > dragToPosition) {
        for (let j = 0; j < sortlist.length; j++) {
            if (sortlist[j] == dragToId) {
                newSortlist[index] = dragFromId;
                index++;
                newSortlist[index] = dragToId;
                index++;
            }
            else {
                if (sortlist[j] == dragFromId) {
                    continue;
                }
                newSortlist[index] = sortlist[j];
                index++;

            }
        }
        sortlist = newSortlist;
        return true;
    }
    if (dragFromPosition < dragToPosition) {
        for (let j = 0; j < sortlist.length; j++) {
            if (sortlist[j] == dragFromId) {
                newSortlist[index] = dragToId;
                index++;
                newSortlist[index] = dragFromId;
                index++;
            }
            else {
                if (sortlist[j] == dragToId) {
                    continue;
                }
                newSortlist[index] = sortlist[j];
                index++;

            }

        }
        sortlist = newSortlist;
        return false;
    }

}

