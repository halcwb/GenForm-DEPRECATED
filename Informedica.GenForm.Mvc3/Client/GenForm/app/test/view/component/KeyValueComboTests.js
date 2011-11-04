Ext.define('GenForm.test.view.component.KeyValueComboTests', {
    describe: 'KeyValueComboShould',

    tests: function () {
        var me = this, testCombo;

        me.getCombo = function () {
            var proxy = Ext.create('Ext.data.proxy.Direct', { directFn: GenForm.server.UnitTest.GetGenericNames}),
                store = { store: Ext.create('GenForm.store.common.KeyValue', {proxy: proxy })};
            if (!testCombo) {
                testCombo = Ext.create('GenForm.lib.view.component.KeyValueCombo', store);
            }
            return testCombo;
        };

        GenForm.test.keyvalueCombo = me.getCombo();

        it('should throw an error when not configured with a store', function () {
            expect(function () {Ext.create('GenForm.lib.view.component.KeyValueCombo');}).toThrow('Combobox needs a store')
        });

        it('be defined', function () {
            expect(me.getCombo()).toBeDefined();
        });
        
    }
});