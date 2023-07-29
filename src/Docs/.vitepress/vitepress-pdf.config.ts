import { defineUserConfig } from "vitepress-export-pdf";

const routeOrder = [
    "/index.html",
    "/introduction.html",
    
    "/lean-startup.html",
    "/discovery-delivery.html",
    "/product-discovery.html",
    "/ddd-modelling-process.html",
    "/delivery-process.html",

    "/getting-started.html",
    "/roadmap.html",
];

export default defineUserConfig({
    outFile: "microscope-boilerplate-documentation.pdf",
    outDir: "./exports",
    pdfOptions: {
        format: "A4",
        printBackground: true,
        margin: {
            bottom: 60,
            left: 25,
            right: 25,
            top: 60,
        },
    },
    sorter: (pageA, pageB) => {
        const aIndex = routeOrder.findIndex(route => route === pageA.path);
        const bIndex = routeOrder.findIndex(route => route === pageB.path);
        return aIndex - bIndex;
    },
});