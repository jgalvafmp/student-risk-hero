import AuthUtil from "../../utils/auth";

const Home = () => {

    const token = localStorage.getItem('token');
    let user = AuthUtil.getTokenData(token);

    return (
        <>
            <h1>Home page</h1>
            <ul>
                <li>User: {user["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"]}</li>
                <li>Role: {user["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"]}</li>
            </ul>
        </>
    );
}

export default Home;