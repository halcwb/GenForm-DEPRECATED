Ext.define('GenForm.lib.util.mixin.FormDataRetriever', {
    getFormData: function () {
        var me = this,
            record = me.getRecord();

        if (!me.isXType('form')) {
            Ext.Error.raise({
                message: 'This mixin can only used by a form',
                source: me
            });
            console.log(me);
        }

        me.getForm().updateRecord(record);
        return record;
    }
});