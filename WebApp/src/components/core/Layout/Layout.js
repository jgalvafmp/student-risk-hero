import Header from "./Header/Header";
import Sidebar from "./Sidebar/Sidebar";
import './Layout.scss';

const Layout = props => {
    return (
        <div className="srhero__layout">
            <Header />
            <div className="srhero__layout--container">
                <Sidebar />
                <div className="srhero__layout--content">
                    {props.children}    
                </div>
            </div>
        </div>
    );
}

export default Layout;