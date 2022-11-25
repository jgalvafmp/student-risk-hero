import Swal from "sweetalert2";

export const SuccessAlert = (title = 'Successful', description = 'Operation have been successful') => {
    Swal.fire(
        title,
        description,
        'success'
    );
}

export const ErrorAlert = (title = 'Error', description = 'Operation have not been completed') => {
    Swal.fire(
        title,
        description,
        'error'
    );
}

export const InfoAlert = (title = 'Error', description = 'Operation have not been completed') => {
    Swal.fire(
        title,
        description,
        'info'
    );
}

export const QuestionAlert = (
    question = 'Are you sure about this changes?', 
    yes = "Yes, I'm sure", 
    no = `Not, don't do it`) => {
    return Swal.fire({
        title: question,
        showDenyButton: true,
        showCancelButton: false,
        confirmButtonText: yes,
        denyButtonText: no,
      });
}