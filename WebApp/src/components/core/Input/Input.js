import React from 'react';
import './Input.scss';

export const Input = props => {
    return (
        <React.Fragment>
            {props.label && <label className='srhero__label'>{props.label}</label>}
            <input
                className='srhero__input'
                type={props.type}
                placeholder={props.placeholder}
                value={props.value}
                onChange={props.onChange}
            />
         </React.Fragment>
    );
}

export default Input;