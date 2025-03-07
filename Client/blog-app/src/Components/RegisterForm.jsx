import React, { useState } from 'react';
import ".././Styles/form.css";
import axios from 'axios';
import InputField from './Inputfield';
import profilePic from ".././Images/profilePic.jpg"

const RegisterForm = () => {
  const [image, setImage] = useState(null);
  const [preview, setPreview] = useState(null);
  const [formData, setFormData] = useState({
    email: '',
    password: '',
    passwordConfirm: '',
    username: '',
    bio: '',
    avatarUrl: ''
  });

  const handleImageChange = (e) => {
    const file = e.target.files[0]; 
    if (file) {
      setImage(file);
      setPreview(URL.createObjectURL(file)); 
      setFormData((prev) => ({
        ...prev,
        avatarUrl: preview
      }));
      console.log(formData.avatarUrl)
    }
  };

  const [error, setError] = useState('');

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
    <div className='form-container'>
      <h2>Register</h2>
      <div className="register-form">
      <form onSubmit={handleSubmit}>
        <div className={true && 'form-row'}>
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
            <label>Username:</label>
            <input
              type="text"
              name="username"
              value={formData.username}
              onChange={handleChange}
              required
            />
          </div>
        </div>
        <div className='formInput'>
          <label>Bio:</label>
          <textarea
            name="bio"
            value={formData.bio}
            onChange={handleChange}
            required
          />
        </div>
        <div className='formAvatar'>
            <label>Profile Picture</label>
            <div className={true && 'form-avatar-row'}>
              <input  
                type="file"
                name='avatarUrl'
                accept="image/*" 
                onChange={handleImageChange} 
              />
              {<img src={preview ? preview : profilePic} alt="Preview" className="image-preview" />}
            </div>
        </div>
        <div className={true && 'form-row'}>
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
        <div className='formInput'>
          <label>Confirm Password:</label>
          <input
            type="password"
            name="passwordConfirm"
            value={formData.passwordConfirm}
            onChange={handleChange}
            required
          />
        </div>
        </div>
        {error && <div style={{ color: 'red' }}>{error}</div>}
        <button type="submit">form</button>
      </form>
      </div>
    </div>
  );
};

export default RegisterForm;
