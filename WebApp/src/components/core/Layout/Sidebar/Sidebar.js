import './Sidebar.scss';
import { NavLink } from 'react-router-dom';

const Sidebar = props => {
    return (
        <div className="srhero__sidebar">
            <ul>
                <li>
                    <NavLink activeClassName='active' to='/users'>Users</NavLink>
                </li>
                <li>
                    <NavLink activeClassName='active' to='/courses'>Courses</NavLink>
                </li>
                <li>
                    <NavLink activeClassName='active' to='/students'>Students</NavLink>
                </li>
            </ul>
        </div>
    );
}

export default Sidebar;