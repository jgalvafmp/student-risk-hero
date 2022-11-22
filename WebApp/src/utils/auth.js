import jwt_decode from "jwt-decode";

const AuthUtil = {
    getTokenData: (token) => {
        if (!token) return null;
        return jwt_decode(token);
    }
}

export default AuthUtil;