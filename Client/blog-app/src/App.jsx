import { useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'
import RegisterForm from './Components/REgisterForm'

function App() {
  const [count, setCount] = useState(0)

  return (
    <>
        <div className='Register'>
          <RegisterForm/>
        </div>
    </>
  )
}

export default App
