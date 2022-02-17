type stateserviceType = {
    token: string;
    sessinoExpires: string;
    user: userType;
    claims: Array<claimType>;
}