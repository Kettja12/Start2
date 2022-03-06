export { getMessage,login, saveUser, savePassword, GetUsers }

let message: string = "";
function getMessage() {
    return message
}


async function login(data):
    Promise<stateserviceType> {
    let response = await apiPost("Account/Login", data);
    if (response.status === "OK") {
        return <stateserviceType>response.data;
    }
    message = response.message;
    return null;
}


async function saveUser(user: userType):
    Promise<userType> {
    var response = await apiPost("account/saveuser", user);
    if (response.status === "OK") {
        return <userType>response.data;
    }
    message = response.message;
    return null;
}

async function savePassword(user: SavePasswordModel):
    Promise<string> {
    var response = await apiPost("account/savePassword", user);
    if (response.status === "OK") {
        return <string>response.data;
    }
    message = response.message;
    return response.message;
}

async function GetUsers(params: userSearchModelType):
    Promise<Array<userType>> {
    var response = await apiPost("account/GetUsers", params);
    if (response.status === "OK") {
        return <Array<userType>>response.data;
    }
    message = response.message;
    return null;
}


