var popUp = function () {
    var _onHideCallback = null;

    return {
        show: function (url, title, callback, width, height) {
            devExPopup.SetContentUrl(url);

            if (title)
                devExPopup.SetHeaderText(title);

            if (callback)
                _onHideCallback = callback;

            devExPopup.Show();

            if (width != null && height != null) {
                devExPopup.SetSize(width, height);
                devExPopup.UpdatePosition();
            }

            var el = devExPopup.GetWindowElement(-1); //Returns the main window element
            var zIndex = $(el).css('z-index');
            $(el).parent().css('z-index', zIndex);
        },

        showDynamic: function (url, title, width, height, callback) {
            devExPopup.SetContentUrl(url);

            if (title)
                devExPopup.SetHeaderText(title);

            if (callback)
                _onHideCallback = callback;

            screenHeight = screen.height;

            if (screenHeight < height + 150) {
                height = height - 250;
            }

            devExPopup.Show();
            devExPopup.SetSize(width, height);
            devExPopup.UpdatePosition();

            var el = devExPopup.GetWindowElement(-1);
            var zIndex = $(el).css('z-index');
            $(el).parent().css('z-index', zIndex);
        },

        hide: function () {
            devExPopup.SetContentUrl('about:blank');
            devExPopup.Hide();

            if (typeof _onHideCallback == 'function')
                _onHideCallback();
        },

        init: function () {
            return false;
        }

    };
} ();