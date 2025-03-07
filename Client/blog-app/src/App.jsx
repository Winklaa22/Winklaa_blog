import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import './App.css'
import RegisterForm from './Components/REgisterForm'
import Home from "./Pages/Home";
import RegisterPage from "./Pages/RegisterPage";
import LoginPage from "./Pages/LoginPage";
import ProfilePage from "./Pages/ProfilePage";

function App() {
  return (
    <>
        <Router>
            <Routes>
                <Route path="/" element={<Home/>}/>
                <Route path="/register" element={<RegisterPage/>}/>
                <Route path="/login" element={<LoginPage/>}/>
                <Route path="/profile" element={<ProfilePage/>}/>
            </Routes>
        </Router>
    </>
  )
}

export default App
