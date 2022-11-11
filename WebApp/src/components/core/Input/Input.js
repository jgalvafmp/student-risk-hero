import React from 'react';
import './Input.scss';

export const Input = props => {
    return (
        <React.Fragment>
            {props.label && <label className='srhero__label'>{props.label}</label>}
            <input
                className={`srhero__input ${props.error ? 'srhero__input--error': ''}`}
                type={props.type}
                placeholder={props.placeholder}
                value={props.value}
                onChange={props.onChange}
            />
            {props.error && <span className='srhero__input--error-label'>{props.error}</span>}
         </React.Fragment>
    );
}

export default Input;