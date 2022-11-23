import useInput from "../../../../hooks/use-input";
import Input from "../../../../components/core/Input/Input";
import Button from "../../../../components/core/Button/Button";

const CourseForm = () => {
    const {
        value: name,
        hasError: nameError,
        isValid: nameIsValid,
        setIsTouched: setNameIsTouched,
        setValue: setNameValue
    } = useInput(value => value.trim() !== '', 'The name input is required');

    const {
        value: description,
        hasError: descriptionError,
        isValid: descriptionIsValid,
        setIsTouched: setDescriptionIsTouched,
        setValue: setDescriptionValue
    } = useInput();

    const formIsValid = nameIsValid && descriptionIsValid;

    const submitHandler = (e) => {
        e.preventDefault();

        if (formIsValid) {
            console.log('submit')
        }
    }

    return (
        <form className="row" onSubmit={submitHandler}>
            <div className="col-xs-12">
                <Input 
                    label="Name" 
                    value={name} 
                    type="text" 
                    placeholder="Type your the course name"
                    error={nameError}
                    onChange={setNameValue}
                    onBlur={setNameIsTouched} />
            </div>
            <div className="col-xs-12">
                <Input 
                    label="Description" 
                    value={description} 
                    type="text" 
                    placeholder="Type your the course description"
                    error={descriptionError}
                    onChange={setDescriptionIsTouched}
                    onBlur={setDescriptionValue} />
            </div>
            <div className="col-xs-12">
                <div style={{width: '150px'}}>
                    <Button type="submit">Submit</Button>
                </div>
            </div>
        </form>
    );
}

export default CourseForm;