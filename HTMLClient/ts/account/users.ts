import { saveUser, GetUsers, getMessage } from "./accountapi.js";
let translations = {
    "No result items in search.": "No result items in search.",
    "Userlist search failed.": "Userlist search failed.",
    "PasswordMissing": "Password missing",
    "PasswordVerificationError": "Salasana and passwordverification are not same.",
}
let userlist:userType[];
let currentuser = -1;

document.addEventListener('DOMContentLoaded', async () => {
    let c = document.getElementById("translations");
    translations = JSON.parse(c.innerHTML);

    if (stateservice == undefined) {
        window.location.href = '/';
    }
    if (stateservice.user.claims !== undefined) {
        if (isAdmin(stateservice.user.claims)===false) {
            window.location.href = '/';
        }
    }
    document.getElementById("search").addEventListener("click", () => search());
    document.getElementById("adduser").addEventListener("click", () => adduser());
    document.getElementById("saveuser").addEventListener("click", () => saveuser());
});

async function search() {
    var ul = document.getElementById('userlist');
    ul.innerHTML = "";
    resetuser();
    var searchfield = (document.querySelector('input[name="searchfield"]:checked') as HTMLInputElement).value;
    let data:userSearchModelType = {
        searchfield: searchfield,
        searchkey: (document.getElementById('searchkey') as HTMLInputElement).value,
    }
    userlist = await GetUsers(data);
    if (userlist !== null && userlist.length>0) {
        setusers();
        return;
    }
    let e = document.getElementById('infomessage');
    let message = getMessage();
    if (message !== "")
        showError(e, translate(translations, message))
    else
        showError(e, translate(translations, "No result items in search."))
};

function setusers() {
    let ul = document.getElementById('userlist');
    userlist.forEach(function (user, index) {
        var li = document.createElement('li');
        var span = document.createElement('span');
        span.style.cssText = "display: inline-block;width:50px"
        span.appendChild(document.createTextNode(user.username));
        li.appendChild(span);
        span = document.createElement('span');
        span.appendChild(document.createTextNode(user.lastName + ", " + user.firstName));
        li.appendChild(span);
        li.setAttribute("index", index.toString());
        li.addEventListener("click", (e) => select(e), false)
        ul.appendChild(li);
    });

}

function adduser() {
    currentuser = -1;
    resetuser();
}

function resetuser() {
    (document.getElementById('username') as HTMLInputElement).value = "";
    (document.getElementById('firstname') as HTMLInputElement).value = "";
    (document.getElementById('lastname') as HTMLInputElement).value = "";
    (document.getElementById('password') as HTMLInputElement).value = "";
    (document.getElementById('passwordverification') as HTMLInputElement).value = "";
    (document.getElementById('isadmin') as HTMLInputElement).checked = false;
}

async function select(e) {
    resetuser();
    var i = e.currentTarget.getAttribute("index");
    currentuser = i;
    (document.getElementById('username') as HTMLInputElement).value = userlist[i].username;
    (document.getElementById('firstname') as HTMLInputElement).value = userlist[i].firstName;
    (document.getElementById('lastname') as HTMLInputElement).value = userlist[i].lastName;
    (document.getElementById('isadmin') as HTMLInputElement).checked = false;
    if (userlist[i].claims != null) {
        userlist[i].claims.forEach(function (claim) {
            if (claim.claimType === "UserGroups" && claim.claimValue.includes("Admin"))
                (document.getElementById('isadmin') as HTMLInputElement).checked = true;
        });
    }

}

async function saveuser() {
    let user: userType = {
        id: 0,
        username: "",
        firstName: "",
        lastName: "",
        claims: []
    };
    var password = (document.getElementById('password') as HTMLInputElement).value;
    var passwordverification = (document.getElementById('passwordverification') as HTMLInputElement).value;
    if (currentuser > -1) {
        user = userlist[currentuser]
    }
    else {
        if (password === "") {
            //showInfoMessage(this.translation["PasswordMissing"], "w3-yellow");
            return;
        }
    }
    if (password !== passwordverification) {
        //showInfoMessage(this.translation["PasswordVerificationError"], "w3-yellow");
        return;

    }
    user.username = (document.getElementById('username') as HTMLInputElement).value;
    user.firstName = (document.getElementById('firstname') as HTMLInputElement).value;
    user.lastName = (document.getElementById('lastname') as HTMLInputElement).value;
    var isset = false;
    var isadmin = (document.getElementById('isadmin') as HTMLInputElement).checked;
    if (user.claims !== null) {
        if (user.claims.length > 0) {
            user.claims.forEach(function (claim) {
                if (claim.claimType === "UserGroups") {
                    if (isadmin) {
                        if (claim.claimValue.includes('Admin') == false) {
                            if (claim.claimValue !== "")
                                claim.claimValue += ',Admin';
                        }
                    }
                    else {
                        if (claim.claimValue.includes('Admin') == true) {
                            claim.claimValue = claim.claimValue.replace('Admin', '');
                        }
                    }
                    isset = true
                }
            });
        }
    }
    if (isset === false && isadmin) {
        if (user.claims === null) {
            user.claims = [];
        }
        user.claims.push(
            {
                "id": 0,
                "claimType": "UserGroups",
                "claimValue": "Admin",
                "userId": user.id
            }
        );
    }
    if (this.currentuser > -1)
        this.userlist[this.currentuser] = user;

    user = await saveUser(user);
    if (user !== null) {
        if (this.currentuser === -1) {
            var ul = document.getElementById('userlist');
            ul.innerHTML = "";
            this.userlist.push(user);
            this.setusers();
        }
        //showInfoMessage(response.message, "w3-green");
        return;
    }
    //showInfoMessage(response.message, "w3-yellow");
};


