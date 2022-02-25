async function apiGet(method) {

    let response = {
        status: "OK",
        message: "OK",
        data: undefined,
    };
    let headers = {
        'Accept': 'application/json',
        'Content-Type': 'application/json',
        'Access-Control-Allow-Origin': '*',
        'Authorization': ''

    };
    if (stateservice !== undefined) {
        let t = stateservice.token;
        if (typeof t != 'undefined')
            headers.Authorization = 'bearer ' + t;
    }

    try {

        let result = await fetch(apiserver+ method, {
            cache: "no-store",
            headers: headers,
            mode: 'cors'
        });
        if (result.ok) {
            let d = await result.json();
            response.data = d;
            return response;
        }
        else {
            response.status = result.status.toString()
            let d = await result.json();
            response.message = d;
            return response;
        }
    }
    catch (err) {
        response.status = "FAIL"
        response.message = err.message;
        return response;
    }
}
async function apiPost(service, data) {
    let response = {
        status: "OK",
        message: "",
        data: undefined,
    };
    let headers = {
        'Accept': 'application/json',
        'Content-Type': 'application/json',
        'Access-Control-Allow-Origin': '*',
        'Authorization':''
    };
    if (stateservice !== undefined) {
        let t = stateservice.token;
        if (typeof t != 'undefined')
            headers.Authorization = 'bearer ' + t;
    }
    try {
        let result = await fetch(apiserver + service, {
            body: JSON.stringify(data),
            cache: "no-store",
            headers: headers,
            method: 'POST'
        });
        if (result.ok) {
            let d = await result.json();
            response.data = d;
            return response;
        }
        else {
            response.status = result.status.toString();
            let d = await result.json();
            response.message = d;
            return response;
        }
    }
    catch (err) {
        response.status = "FAIL"
        response.message =  err.message;
        return response;
    }
}
