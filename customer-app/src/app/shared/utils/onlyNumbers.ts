const onlyNumbers = (text:string)=>[].filter.call(text, (ch) => !isNaN(ch)).join('');
export default onlyNumbers;