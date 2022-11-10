import Header from "./Header/Header";
import Sidebar from "./Sidebar/Sidebar";

const Layout = props => {
    return (
        <div>
            <Header />
            <Sidebar />
            {props.children}
        </div>
    );
}

export default Layout;