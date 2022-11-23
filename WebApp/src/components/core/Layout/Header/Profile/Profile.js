import { useContext } from 'react';
import { Link } from 'react-router-dom';

import profileMan from '../../../../../assets/images/man.png';
import profileWoman from '../../../../../assets/images/woman.png';
import AuthContext from '../../../../../store/auth-context';
import AuthUtil from '../../../../../utils/auth';

import '../Header.scss';

const Profile = props => {
    const authCtx = useContext(AuthContext);
    const profile = AuthUtil.getTokenData(authCtx.token);

    let profileContent = <div className="srhero__profile"><Link to='/login'>You need to login</Link></div>

    if (profile) {
        profileContent = (
            <div className="srhero__profile">
                <img alt="Profile" src={props.gender === "Female" ? profileWoman : profileMan} />
                <div>
                    <span>{profile["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"]}</span>
                    <span className='label-user-profile'>{profile["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"]}</span>
                </div>
            </div>
        )
    }

    return profileContent;
}

export default Profile;