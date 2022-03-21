import Dummy from "./dummy";

test('dummy test', () => {
    const dummy = new Dummy()
    expect(dummy.getDummyValue()).toEqual("I'm alive");
})