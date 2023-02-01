/** @type {import('tailwindcss').Config} */
module.exports = {
    content: [
        "../**/*.{razor,html}",
    ],
    theme: {
        extend: {
            colors: {
                "title-blue": "#99cccc",
            },
            container: {
                center: true,
            },
        },
    },
    plugins: [],
};
