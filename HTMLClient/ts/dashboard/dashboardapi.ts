export { getDashboardItems }

async function getDashboardItems():
    Promise<Array<dashboardItemType>> {
    let response = await apiGet("Dashboard/GetDashboardItems");
    if (response.status === "OK") {
        return  <Array<dashboardItemType>>response.data;
    }
}
