import React, { useState } from "react";
import Button from "../../../components/core/Button/Button";
import Modal from "../../../components/core/Modal/Modal";
import CourseForm from "./CourseForm/CourseForm";

const CoursePage = () => {
    const [openForm, setOpenForm] = useState(false);

    const cancelHandler = () => {
        setOpenForm(false)
    }

    const form = <Modal 
                    title={"Course form"}
                    onCancel={cancelHandler}
                >
                    <CourseForm />
                </Modal>

    return (
        <React.Fragment>
            {openForm && form}
            <div className="row">
                <div className="col-xs-6">
                    <h1>Course page</h1>
                </div>
                <div className="col-xs-6 align-end">
                    <div style={{width: "200px" }}>
                        <Button onClick={() => { setOpenForm(true) }}>
                            New course
                        </Button>
                    </div>
                </div>
                <div className="col-xs-12">
                    <table className="">
                    </table>
                </div>
            </div> 
        </React.Fragment>
    );
}

export default CoursePage;