import { useState } from "react";

const useInput = (validation, errorMessage) => {
    const [value, setValue] = useState('');
    const [isTouched, setIsTouched] = useState(false);

    const isValid = validation ? validation(value) : true;
    const hasError = !isValid && isTouched && errorMessage ? errorMessage : undefined;

    const setValueHandler = (event) => {
        setValue(event.target.value)
    }

    const setBlurHandler = () => {
        setIsTouched(true);
    }

    const resetHandler = () => {
        setIsTouched(false);
        setValue('');
    }

    return {
        value,
        isValid,
        hasError,
        setValue: setValueHandler,
        setIsTouched: setBlurHandler,
        reset: resetHandler
    }
}

export default useInput;