Ext.define('GenForm.test.util.MessageChecker', {
    queryHelper: Ext.create('GenForm.test.util.QueryHelper'),

    checkLoginMessage: function (message) {
        var me = this, msgBox = me.queryHelper.getWindow('messagebox');
        if (msgBox)
        {
            if (msgBox.cfg) {
                if (msgBox.cfg.msg === message)
                {
                    me.queryHelper.getButton('messagebox', 'button[text=OK]').btnEl.dom.click();
                    return true;
                }
            }
        }
        return false;
    }

});