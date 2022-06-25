import { render, screen } from "@testing-library/react";
import App from "./App";

test("Verify client react app is loaded", () => {
  render(<App />);
  const titleElement = screen.getByText(/Wordle like Dojo/i);
  expect(titleElement).toBeInTheDocument();
});
