Ext.define('GenForm.test.util.MessageChecker', {
    queryHelper: null,

    setQueryHelper: function () {
        var me = this;
        me.queryHelper = Ext.create('GenForm.test.util.QueryHelper');
    },

    checkMessage: function (message) {
        var me = this, msgBox;
        console.log(message);
        if(!me.queryHelper) me.setQueryHelper();
        msgBox = me.queryHelper.getWindow('messagebox');

        if (msgBox)
        {
            if (msgBox.cfg) {
                if (msgBox.cfg.msg === message)
                {
                    console.log(message);
                    me.queryHelper.clickButton(me.queryHelper.getButton('messagebox', 'OK'));
                    return true;
                }
            }
        }
        return false;
    }

});