Ext.define('GenForm.test.util.FormDataRetrieverTests', {
    describe: 'FormDataRetrieverShould',

    tests: function () {
        var me = this;

        Ext.define('TestRetrieverForm', {
            extend: 'Ext.form.Panel',

            requires: [
                'GenForm.model.product.Substance'
            ],

            mixins: [
                'GenForm.lib.util.mixin.FormDataRetriever'
            ],

            items: [
                { xtype: 'textfield', name: 'SubstanceName' }
            ]
        });

        me.getTestForm = function () {
            var form =  Ext.create('TestRetrieverForm');
            form.loadRecord(Ext.create('GenForm.model.product.Substance', {
                SubstanceName: 'paracetamol'
            }));
            return form;
        };
        
        it('that a test form with a dataretriever can be created', function () {
            expect(me.getTestForm().isXType('form')).toBeTruthy();
        });

        it('that dataretriever adds a getFormData method', function () {
            expect(me.getTestForm().getFormData).toBeDefined();
        });

        it('getFormData returns a populated model', function () {
            expect(me.getTestForm().getFormData().data.SubstanceName).toBe('paracetamol');
        });

    }

});