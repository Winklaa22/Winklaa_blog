import ".././Styles/InputField.css";
const InputField = (
    {
        headerText= '', 
        onChange, 
        value, 
        placeholder = '', 
        hasMaxLength = false, 
        maxLength = 255, 
        hasMaxLengthCounter = false, 
        type="text",
        name=""}) =>{
    return (
    <label>
        {headerText && {headerText}}
        <input
            value={value}
            onChange={e => onChange(e)} 
            maxLength={maxLength}
            placeholder={placeholder}
            type={type}
            name={name}
            id="item"
        />
        { hasMaxLengthCounter &&
        <div style={{ fontSize: '0.8rem', color: 'gray' }}>
            {value.length}/{maxLength} characters
        </div>
        }
    </label>
    );
}

export default InputField;