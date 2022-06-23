import Letter from "./Letter";
import "./Line.css";

function Line(props) {
  let content;

  if (props.line) {
    content = (
      <div className="Line">
        {props.line.map((letter, indx) => <Letter key={indx} letter={letter} />)}
      </div>
    );
  } else {
    content = (
      <div className="Line">
        <Letter />
        <Letter />
        <Letter />
        <Letter />
        <Letter />
      </div>
    );
  }

  return (
    <div className="Line">
      {content}
    </div>
  );
}

export default Line;
