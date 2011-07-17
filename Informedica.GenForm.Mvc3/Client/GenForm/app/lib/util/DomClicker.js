Ext.define('GenForm.lib.util.DomClicker', {

    click: function (domelement) {
        var me = this;
        if (!domelement.click) {
            me.createClick(domelement);
        }  else {
            domelement.click();
        }
    },

    createClick: function (domelement) {
        var mouseEvent = document.createEvent("MouseEvent");

        mouseEvent.initMouseEvent("click", true, true, window, 0, 0, 0, 0, 0, false, false, false, 0, null);
        domelement.dispatchEvent(mouseEvent);
    }
});