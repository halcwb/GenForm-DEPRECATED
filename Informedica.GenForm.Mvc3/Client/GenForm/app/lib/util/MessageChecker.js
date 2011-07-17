Ext.define('GenForm.lib.util.MessageChecker', {
    queryHelper: null,

    setQueryHelper: function () {
        var me = this;
        me.queryHelper = Ext.create('GenForm.lib.util.QueryHelper');
    },

    checkMessage: function (message) {
        var me = this, msgBox;
        if(!me.queryHelper) me.setQueryHelper();
        msgBox = me.queryHelper.getWindow('messagebox');

        if (msgBox)
        {
            if (msgBox.cfg) {
                if (msgBox.cfg.msg === message)
                {
                    me.queryHelper.clickButton(me.queryHelper.getButton('messagebox', 'OK'));
                    return true;
                }
            }
        }
        return false;
    }

});