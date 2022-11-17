import React from 'react';
import './Input.scss';

export const Input = props => {
    let inputForm = (
        <input
            className={`srhero__input ${props.error ? 'srhero__input--error': ''}`}
            type={props.type}
            placeholder={props.placeholder}
            value={props.value}
            onChange={props.onChange}
        />
    );

    if (props.type === "dropdown") {
        inputForm = (
            <select
                className={`srhero__input ${props.error ? 'srhero__input--error': ''}`}
                value={props.value}
                onChange={props.onChange}
            >
                <option value={undefined}>{props.placeholder}</option>
                {props.children}
            </select>
        );
    }

    return (
        <div className='srhero__input--container'>
            {props.label && <label className='srhero__label'>{props.label}</label>}
            {inputForm}
            {props.error && <span className='srhero__input--error-label'>{props.error}</span>}
         </div>
    );
}

export default Input;