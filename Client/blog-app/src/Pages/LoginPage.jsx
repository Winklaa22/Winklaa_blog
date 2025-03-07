import Header from "../Components/Header"
import RegisterForm from "../Components/REgisterForm"
import ".././Styles/RegisterLoginPage.css"
import LoginForm from "../Components/LoginForm"

const LoginPage = () =>{
    return(
        <div>
            <Header/>
            <div className="container">
                <LoginForm/>
            </div>
        </div>
    )
}

export default LoginPage