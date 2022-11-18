import jwt_decode from "jwt-decode";

const AuthUtil = {
    getTokenData: (token) => {
        return jwt_decode(token);
    }
}

export default AuthUtil;