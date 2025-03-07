import Header from "../Components/Header"
import RegisterForm from "../Components/REgisterForm"
import ".././Styles/RegisterLoginPage.css";

const RegisterPage = () =>{
    return(
        <>
            <Header/>
            <div className="container">
                <RegisterForm/>
            </div>
            
        </>
    )
}

export default RegisterPage