Ext.define('GenForm.test.extjs.ComponentQueryTests', {
    describe: 'Ext.ComponentQuery',
    tests: function () {
        var createTestForm, findForm;

        createTestForm = function () {
            return Ext.create('Ext.form.Panel', {
                id: 'testform',
                title: 'test',
                items: [
                    { xtype: 'textfield', name: 'test'}
                ]
            });
        };

        findForm = function () {
            return Ext.ComponentQuery.query('#testform');
        };

        it('a test form should be created', function () {
           expect(createTestForm()).toBeDefined();
        });

        it('component query finds the panel', function () {
           expect(findForm().length).toBe(1);
        });

        it('should be able to get the testform using the title', function () {
           expect(Ext.ComponentQuery.query('panel[title="test"]').length).toBe(1);
        });

        it('if you go down from testform you get a textfield', function () {
            expect(findForm()[0].down('textfield').name).toBe('test');
        });
    }
});