module.exports = {
  darkMode: "class",
  content: [
    "./Views/**/*.cshtml",
    "./Helper/Css.cs",
    "./TagHelpers/*.cs",
    "node_modules/preline/dist/*.js",
    "./wwwroot/**/*.js",
  ],
  theme: {
    extend: {
      colors: {
        primary: {
          300: "#f9aea8",
          400: "#f37f76",
          500: "#d63b2f",
          600: "#c23127",
          700: "#a1231a",
        },
        secondary: {
          300: "#dbe784",
          400: "#c2d447",
          500: "#a4ba28",
          600: "#7f941c",
          700: "#60711a",
        },
        error: {
          400: "#d27f81",
          500: "#c76c6e",
          600: "#b15153",
        },
        success: {
          300: "#c1dd97",
          400: "#96c358",
          500: "#78a83a",
        },
      },
    },
  },
  plugins: [require("preline/plugin")],
};
