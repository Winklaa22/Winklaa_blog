import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import './App.css'
import RegisterForm from './Components/REgisterForm'
import Home from "./Pages/Home";
import RegisterPage from "./Pages/RegisterPage";

function App() {
  return (
    <>
        <Router>
            <Routes>
                <Route path="/" element={<Home/>}/>
                <Route path="/register" element={<RegisterPage/>}/>

            </Routes>
        </Router>
    </>
  )
}

export default App
