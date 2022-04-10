export {
    getMessage,
    getDashboardItems,
    saveDashboardItem,
    getItem1,
    getItem2,
    getItem3
}

let message: string = "";
function getMessage() {
    return message
}
async function getDashboardItems():
    Promise<Array<dashboardItemType>|null> {
    let response = await apiGet("Dashboard/GetDashboardItems");
    if (response.status === "OK") {
        return  <Array<dashboardItemType>>response.data;
    }
    message = response.message;
    return null;
}

async function saveDashboardItem(control: dashboardItemType):
    Promise<dashboardItemType|null> {
    var response = await apiPost("Dashboard/SaveDashboardItem", control);
    if (response.status === "OK") {
        return <dashboardItemType>response.data;
    }
    message = response.message;
    return null;
}

async function getItem1():
    Promise<Item1Type|null> {
    let response = await apiPost("Dashboard/GetItem1", {});
    if (response.status === "OK") {
        return <Item1Type>response.data;
    }
    message = response.message;
    return null;
}

async function getItem2(data:any):
    Promise<Item2Type | null> {
    let response = await apiPost("Dashboard/GetItem2",data);
    if (response.status === "OK") {
        return <Item2Type>response.data;
    }
    message = response.message;
    return null;
}

async function getItem3(data: Item3Type):
    Promise<Item3Type | null> {
    let response = await apiPost("Dashboard/GetItem3", data);
    if (response.status === "OK") {
        return <Item3Type>response.data;
    }
    message = response.message;
    return null;
}





