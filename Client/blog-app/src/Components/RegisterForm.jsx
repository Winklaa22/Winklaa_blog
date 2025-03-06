import React, { useState } from 'react';
import ".././Styles/RegisterForm.css";
import axios from 'axios';

const RegisterForm = () => {
  const [formData, setFormData] = useState({
    email: '',
    password: '',
    passwordConfirm: '',
    username: '',
    bio: '',
    avatarUrl: ''
  });

  const [error, setError] = useState('');

  // Funkcja obsługująca zmiany w formularzu
  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData({
      ...formData,
      [name]: value
    });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    
    if (formData.password !== formData.passwordConfirm) {
      setError('Passwords do not match');
      return;
    }

    try {
      const response = await axios.post('http://localhost:5000/Auth/Register', formData);
      alert('Registration successful');
      console.log(response.data);
    } catch (err) {
      setError(err.response.data);
      console.error(err.response.data);
    }
  };

  return (
    <div className='register-container'>
      <h2>Register</h2>
      <div className="register-form">
      <form onSubmit={handleSubmit}>
        <div className='registerInput'>
          <label>Email:</label>
          <input
            type="email"
            name="email"
            value={formData.email}
            onChange={handleChange}
            required
          />
        </div>
        <div className='registerInput'> 
          <label>Username:</label>
          <input
            type="text"
            name="username"
            value={formData.username}
            onChange={handleChange}
            required
          />
        </div>
        <div className='registerInput'>
          <label>Bio:</label>
          <textarea
            name="bio"
            value={formData.bio}
            onChange={handleChange}
            required
          />
        </div>
        <div className='registerInput'>
          <label>Avatar URL:</label>
          <input
            type="url"
            name="avatarUrl"
            value={formData.avatarUrl}
            onChange={handleChange}
          />
        </div>
        <div className='registerInput'>
          <label>Password:</label>
          <input
            type="password"
            name="password"
            value={formData.password}
            onChange={handleChange}
            required
          />
        </div>
        <div className='registerInput'>
          <label>Confirm Password:</label>
          <input
            type="password"
            name="passwordConfirm"
            value={formData.passwordConfirm}
            onChange={handleChange}
            required
          />
        </div>
        {error && <div style={{ color: 'red' }}>{error}</div>}
        <button type="submit">Register</button>
      </form>
      </div>
    </div>
  );
};

export default RegisterForm;
