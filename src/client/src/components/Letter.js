import "./Letter.css";

function Letter({ letter }) {

  let colorClass = "Letter"   
  if (letter) {
    if(letter.validity === 1) {
      colorClass += " Green"
    }
    else if (letter.validity === 2) {
      colorClass += " Orange"
    }
  }

  return (
    <div className={colorClass}>
      {letter?.value}
    </div>
  );
}

export default Letter;
