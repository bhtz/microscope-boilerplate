window.jsInterop = {

    getCultureCookie: function () {
        // return document.cookie.replace(/(?:(?:^|.*;\s*)culture\s*\=\s*([^;]*).*$)|^.*$/, "$1");
        return this.getCookie('.AspNetCore.Culture');
    },

    getCookie: function (cname) {
        let name = cname + "=";
        let decodedCookie = decodeURIComponent(document.cookie);
        let ca = decodedCookie.split(';');
        for (let i = 0; i < ca.length; i++) {
            let c = ca[i];
            while (c.charAt(0) == ' ') {
                c = c.substring(1);
            }
            if (c.indexOf(name) == 0) {
                return c.substring(name.length, c.length);
            }
        }
        return "";
    },

    async downloadFileFromUrl(url, filename) {
        const response = await fetch(url);
        if (!response.ok) throw new Error("Failed to download file.");

        const blob = await response.blob();
        const downloadUrl = URL.createObjectURL(blob);

        const a = document.createElement('a');
        a.href = downloadUrl;
        a.download = filename;
        document.body.appendChild(a);
        a.click();
        document.body.removeChild(a);

        URL.revokeObjectURL(downloadUrl);
    }
};