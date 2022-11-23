import { useContext } from "react";
import { useHistory } from "react-router-dom";
import AuthContext from "../../store/auth-context";


const Home = () => {
    const authCtx = useContext(AuthContext);
    const history = useHistory();

    console.log('HOME COMPONENT')
    console.log(!authCtx.isLoggedIn)

    if(!authCtx.isLoggedIn) {
        history.replace('/login');
    }

    return (
        <>
            <h1>Home page</h1>
        </>
    );
}

export default Home;