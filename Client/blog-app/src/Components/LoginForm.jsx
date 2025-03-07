import axios from "axios";
import ".././Styles/form.css";
import { useState } from "react";
const LoginForm = () => {

    const [error, setError] = useState('');
    const[token, setToken] = useState('')
    const [formData, setFormData] = useState(
        {
            email: '',
            password: ''
        }
    )

    const handleChange = (e) =>{
        const { name, value } = e.target
        setFormData({
            ...formData,
            [name]: value
        })
    }

    const handleSubmit = async (e) =>{
        e.preventDefault()

        try{
            const response = await axios.post('http://localhost:5000/Auth/Login', formData, {
                headers:{
                    'Content-Type': 'application/json',
                }
            })
            if (response.data.token) {
                localStorage.setItem('authToken', response.data.token);
                console.log(response.data.token)
            }
            alert('Login successful', response.data);
        } catch(err) {
            if (error.response) {
                setError(error.response.data.message || 'Login failed');
            } else {
                setError('Something went wrong. Please try again.');
            }
        }
    }

    return (
    <div className='form-container'>
        <h2>Login</h2>
        <div className="login-form">
            <form onSubmit={handleSubmit}>
                <div className='formInput'>
                    <label>Email:</label>
                        <input
                            type="email"
                            name="email"
                            value={formData.email}
                            onChange={handleChange}
                            required
                    />
                </div>
                <div className='formInput'>
                    <label>Password:</label>
                    <input
                        type="password"
                        name="password"
                        value={formData.password}
                        onChange={handleChange}
                        required
                    />
                </div>
                <div className="form-addic-row">
                    <label>
                        <input id="remember_me" name="remember_me" type="checkbox" value="y"/>Remember me 
                    </label>
                    <a href="">Forgot Password?</a>
                </div>

                <button type="submit">Submit</button>
            </form>
        </div>
    </div>)
}

export default LoginForm