import './Header.scss';
import Profile from './Profile/Profile';
import logo from '../../../../assets/images/Logo.png';
import { Link } from 'react-router-dom';
import { useContext } from 'react';
import AuthContext from '../../../../store/auth-context';

const Header = () => {

    const authCtx = useContext(AuthContext);

    const logoutHandler = () => {
        authCtx.logout();
    }

    return (
        <div className="srhero__header">
            <div className='srhero__header--logo'>
                <Link to='/'><img src={logo} alt="Logo" /></Link>
                <Link to='/'>Student Risk Hero</Link>
            </div>
            <div className='srhero__header--user-profile'>
                <Profile />
                <button onClick={logoutHandler}>Log out</button>
            </div>
        </div>
    );
}

export default Header;