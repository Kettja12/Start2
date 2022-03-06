export {
    getMessage,
    getDashboardItems,
    saveDashboardItem,
    getItem1
}

let message: string = "";
function getMessage() {
    return message
}
async function getDashboardItems():
    Promise<Array<dashboardItemType>> {
    let response = await apiGet("Dashboard/GetDashboardItems");
    if (response.status === "OK") {
        return  <Array<dashboardItemType>>response.data;
    }
    message = response.message;
    return null;
}

async function saveDashboardItem(control: dashboardItemType):
    Promise<dashboardItemType> {
    var response = await apiPost("Dashboard/SaveDashboardItem", control);
    if (response.status === "OK") {
        return <dashboardItemType>response.data;
    }
    message = response.message;
    return null;
}

async function getItem1():
    Promise<Item1Type> {
    let response = await apiPost("Dashboard/GetItem1", {});
    if (response.status === "OK") {
        return <Item1Type>response.data;
    }
    message = response.message;
    return null;
}



