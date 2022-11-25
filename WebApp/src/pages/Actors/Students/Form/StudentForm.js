import useInput from "../../../../hooks/use-input";
import Input from "../../../../components/core/Input/Input";
import Button from "../../../../components/core/Button/Button";
import useHttp from "../../../../hooks/use-http";
import { SuccessAlert } from "../../../../services/AlertService";
import { useEffect, useState } from "react";

const StudentForm = (props) => {
    const api = "students";
    const module = "Student";

    const [currentEntity, setCurrentEntity] = useState(undefined);
    const [courses, setCourses] = useState([]);

    const {
        value: name,
        hasError: nameError,
        isValid: nameIsValid,
        setIsTouched: setNameIsTouched,
        setValue: setNameValue
    } = useInput(value => value.trim() !== '', 'The firstname input is required');

    const {
        value: lastname,
        hasError: lastnameError,
        isValid: lastnameIsValid,
        setIsTouched: setlastnameIsTouched,
        setValue: setlastnameValue
    } = useInput(value => value.trim() !== '', 'The lastname input is required');

    const {
        value: birthdate,
        hasError: birthdateError,
        isValid: birthdateIsValid,
        setIsTouched: setbirthdateIsTouched,
        setValue: setbirthdateValue
    } = useInput(value => value.trim() !== '', 'The birthdate input is required');

    const {
        value: course,
        hasError: courseError,
        isValid: courseIsValid,
        setIsTouched: setcourseIsTouched,
        setValue: setcourseValue
    } = useInput(value => value.trim() !== '', 'The course is required');

    const http = useHttp();

    const fetchData = async (url) => {   
        const response = await http.sendRequest({ url: api+'/'+props.id });

        if(response.ok) {
            const data = await response.json();
            setCurrentEntity(data);
            setNameValue({ target: { value: data.firstname}});
            setlastnameValue({ target: { value: data.lastname}});
            setbirthdateValue({ target: { value: data.birthdate.split('T')[0]}});
            setcourseValue({ target: { value: data.course}});
        }
    };

    const fetchCoursesData = async () => {   
        const response = await http.sendRequest({ url: 'courses' });

        if(response.ok) {
            const data = await response.json();
            setCourses(data);
        }
    };

    useEffect(() => {
        if(props.id) {
            fetchData();
        }
        fetchCoursesData();
        // eslint-disable-next-line
    }, [])


    const formIsValid = nameIsValid && lastnameIsValid && birthdateIsValid && courseIsValid;

    const submitHandler = (e) => {
        e.preventDefault();

        if (formIsValid) {
            if(props.id) {
                const data = {
                    ...currentEntity, 
                    firstname: name, 
                    lastname: lastname,
                    course: course,
                    birthdate: birthdate,
                }

                http.sendRequest({ url: api }, data, 'PUT').then(() => {
                    SuccessAlert('Operation completed', module+' have been updated successfully');
                    props.submit();
                });
            }else {
                const data = {
                    firstname: name, 
                    lastname: lastname,
                    course: course,
                    birthdate: birthdate,
                    fathersFullName: '',
                    mothersFullName: '',
                    phoneNumber1: '',
                    phoneNumber2: '',
                    profilePicture: ''
                }

                http.sendRequest({ url: api }, data, 'POST').then(() => {
                    SuccessAlert('Operation completed', module+' have been created successfully');
                    props.submit();
                });
            }
        } else {
            setNameIsTouched(true);
            setbirthdateIsTouched(true);
            setcourseIsTouched(true);
            setlastnameIsTouched(true);
        }
    }

    return (
        <form className="row" onSubmit={submitHandler}>
            <div className="col-xs-12">
                <Input 
                    label="Firstname" 
                    value={name} 
                    type="text" 
                    placeholder="Type your the firstname"
                    error={nameError}
                    onChange={setNameValue}
                    onBlur={setNameIsTouched} />
            </div>
            <div className="col-xs-12">
                <Input 
                    label="Lastname" 
                    value={lastname} 
                    type="text" 
                    placeholder="Type your the lastname"
                    error={lastnameError}
                    onChange={setlastnameValue}
                    onBlur={setlastnameIsTouched} />
            </div>
            <div className="col-xs-12">
                <Input 
                    label="Course" 
                    value={course} 
                    type="dropdown" 
                    placeholder="Select your the course"
                    error={courseError}
                    onChange={setcourseValue}
                    onBlur={setcourseIsTouched}>
                    {courses.map(res => {
                        return <option value={res.id}>{res.name}</option>
                    })}
                </Input>
            </div>
            <div className="col-xs-12">
                <Input 
                    label="Birthdate" 
                    value={birthdate} 
                    type="date" 
                    placeholder="Type your the Birthdate"
                    error={birthdateError}
                    onChange={setbirthdateValue}
                    onBlur={setbirthdateIsTouched} />
            </div>
            <div className="col-xs-12">
                <div style={{width: '150px'}}>
                    <Button type="submit">Submit</Button>
                </div>
            </div>
        </form>
    );
}

export default StudentForm;